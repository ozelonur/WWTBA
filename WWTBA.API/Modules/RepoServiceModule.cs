using System.Reflection;
using Autofac;
using WWTBA.Core.Repositories;
using WWTBA.Core.Services;
using WWTBA.Core.UnitOfWorks;
using WWTBA.Repository;
using WWTBA.Repository.Repositories;
using WWTBA.Repository.UnitOfWorks;
using WWTBA.Service.Mapping;
using WWTBA.Service.Services;
using WWTBA.Shared.Interfaces;
using WWTBA.Shared.Services;
using Module = Autofac.Module;

namespace WWTBA.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<,>)).As(typeof(IService<,>))
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            // builder.RegisterType<LessonService>().As<ILessonService>().InstancePerLifetimeScope();
            // builder.RegisterType<SubjectService>().As<ISubjectService>().InstancePerLifetimeScope();
            // builder.RegisterType<QuestionService>().As<IQuestionService>().InstancePerLifetimeScope();
            // builder.RegisterType<AnswerService>().As<IAnswerService>().InstancePerLifetimeScope();
            
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();

            Assembly apiAssembly = Assembly.GetExecutingAssembly();
            Assembly repositoryAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            Assembly serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}

