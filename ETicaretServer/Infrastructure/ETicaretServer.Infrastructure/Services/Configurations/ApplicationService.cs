using ETicaretServer.Application.Configurations;
using ETicaretServer.Application.CustomAttributes;
using ETicaretServer.Application.DTOs.Configurations;
using ETicaretServer.Application.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using Action = ETicaretServer.Application.DTOs.Configurations.Action;

namespace ETicaretServer.Infrastructure.Services.Configurations
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndPoints(Type type)
        {
            List<Menu> menus = new();
            Assembly assembly = Assembly.GetAssembly(type);
            // Controllerbase den uretilen controllerlari elde ettik
            var controllers = assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(ControllerBase)));
            if (controllers != null)
            {
                foreach (var controller in controllers)
                {
                    var actions = controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefinitionAttribute)));
                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                            var attributes = action.GetCustomAttributes(true);
                            if(attributes != null)
                            {
                                Menu menu = null;
                              var authorizeDefinitionAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
                                if(!menus.Any(m => m.Name == authorizeDefinitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefinitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                {
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefinitionAttribute.Menu);
                                }

                                Action _action = new()
                                {
                                    ActionType = Enum.GetName(typeof(ActionType), authorizeDefinitionAttribute.ActionType),
                                    Definition = authorizeDefinitionAttribute.Definition
                                };
                                // Http -> methodları ogrenmek icin (post-put-get... )
                                var httpAttributes = attributes.FirstOrDefault(a => a.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;

                                if(httpAttributes != null)
                                {
                                    _action.HttpType = httpAttributes.HttpMethods.First();
                                }
                                else
                                {
                                    _action.HttpType = "GET";
                                }

                                _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";
                                menu.Actions.Add(_action);
                            }

                        }
                    }
                }
            }


            return menus;
        }
    }
}
