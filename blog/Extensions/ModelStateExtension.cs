using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace blog.Extensions;

// por padrão as classes extendidas precisão ser estáticas, e seus métodos também.
public static class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelState)
    {
        var result = new List<string>();

        // foreach(var item in modelState.Values)
        // {
        //     foreach(var errorsState in item.Errors)
        //     {
        //         result.Add(errorsState.ErrorMessage);
        //     }
        // }

        foreach (var item in modelState.Values)
            result.AddRange(item.Errors.Select(x => x.ErrorMessage));


        return result;
    }
}
