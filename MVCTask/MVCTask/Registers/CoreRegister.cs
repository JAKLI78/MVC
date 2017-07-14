using MVCTask.Core.Interface;
using MVCTask.Core.Services;
using StructureMap.Configuration.DSL;

namespace MVCTask.Registers
{
    public class CoreRegister : Registry
    {
        public CoreRegister()
        {
            For<ITitlesServise>().Use<TitleService>();
            For<IImageService>().Use<ImageService>();
            For<ICompanyService>().Use<CompanyService>();
            For<IUserService>().Use<UserService>();
            For<ISearchService>().Use<SearchService>();
        }
    }
}