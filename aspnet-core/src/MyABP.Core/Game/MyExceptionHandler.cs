using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Exceptions;
using Abp.Events.Bus.Handlers;
using MyABP.Shop;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyABP.Game
{
    public class MyExceptionHandler : IEventHandler<AbpHandledExceptionData>, ITransientDependency
    {
        public readonly IRepository<Address> _addressRepository;
        public MyExceptionHandler(IRepository<Address> addressRepository)
        {
            this._addressRepository = addressRepository;
        }

        public async void  HandleEvent(AbpHandledExceptionData eventData)
        {
            int a = 1;
            //TODO: Check eventData.Exception!
        }
    }
}
