using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Kernel;
using Moq;

namespace TestProject1
{
    public class AutoMockSpecimenFactory : ISpecimenBuilder
    {
        private readonly IDictionary<Type, Mock?> _mockTypeLookup = new Dictionary<Type, Mock?>();
        
        public object? Create(object request, ISpecimenContext context)
        {
            var underlyingRequest = (request as SeededRequest)?.Request;
            if (underlyingRequest == null)
                throw new ArgumentException("Could not determine underlying object type", nameof(request));

            var requestType = (Type) underlyingRequest;
            if (IsGenericMock(requestType) && _mockTypeLookup.TryGetValue(requestType.GetGenericArguments()[0], out var existingMock))
                return existingMock;

            var constructorInfo = requestType.GetConstructors().FirstOrDefault();
            if (constructorInfo == null)
                throw new ArgumentException("Cannot determine constructor to use.");

            var requiredConstructorParameters = constructorInfo.GetParameters();
            var constructorParameters = new object?[requiredConstructorParameters.Length];
            for (var i = 0; i < requiredConstructorParameters.Length; i ++)
            {
                var parameterType = requiredConstructorParameters[i].ParameterType;
                if (!_mockTypeLookup.TryGetValue(parameterType, out var parameterMockValue))
                {
                    var creator = typeof(Mock<>).MakeGenericType(parameterType);
                    parameterMockValue = (Mock?) Activator.CreateInstance(creator);
                    _mockTypeLookup.Add(parameterType, parameterMockValue);
                }
                
                constructorParameters[i] = parameterMockValue?.Object;
            }

            var requestObject = constructorInfo.Invoke(constructorParameters);
            if (IsGenericMock(requestType))
                _mockTypeLookup.Add(requestType.GetGenericArguments().First(), (Mock) requestObject);

            return requestObject;
        }
        
        private bool IsGenericMock(Type requestType) => requestType.IsGenericType && requestType.GetGenericTypeDefinition() == typeof(Mock<>);
    }
}