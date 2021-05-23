﻿using DemoCrud.ViewModels;
using System.Web;
using System.Web.Mvc;

namespace DemoCrud.Infra
{
    public class ParametrosPaginacaoModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            ParametrosPaginacao parametrosPaginacao = new ParametrosPaginacao(request.Form);

            return parametrosPaginacao;
        }
    }
}