/***********************************************************
 * RUSSIAN MAFIA TEAM : vk.com/skynetdz | www.dayzmafia.ru
 * ZHive (DAYZ SA)
 * Copyright: 2015-2017 Pavel Kirikov
 ***********************************************************/

using System.Collections.Generic;
using System.Web.Http.Dispatcher;

namespace ZHive.WebAPI
{
    public class AssembliesResolve : DefaultAssembliesResolver
    {
        public override ICollection<System.Reflection.Assembly> GetAssemblies()
        {
            var assemblies = base.GetAssemblies();
            var customControllersAssembly = typeof(ZHive.WebAPI.Controllers.HiveController).Assembly;

            if (!assemblies.Contains(customControllersAssembly))
                assemblies.Add(customControllersAssembly);

            return assemblies;
        }    
    }
}
