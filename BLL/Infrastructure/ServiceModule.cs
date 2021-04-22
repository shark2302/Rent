﻿using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DAL.Interfaces;
namespace BLL.Infrastructure
{
    class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
