﻿using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ContosoUniversity.DAL.Repositories;

namespace ContosoUniversity
{
    public static class DependenciesConfig
    {
        public static IContainer RegisterDependencies(ContainerBuilder builder = null)
        {
            if(builder == null) 
                builder = new ContainerBuilder();

            ConfigureDependencies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }

        public static void ConfigureDependencies(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<CourseRepository>().As<ICourseRepository>();//.InstancePerRequest();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>();
            builder.RegisterType<InstructorRepository>().As<IInstructorRepository>();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();//.InstancePerRequest();
        }
    }
}