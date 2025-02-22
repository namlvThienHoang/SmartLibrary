﻿using SmartLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SmartLibrary.Utilities.Helpers.Commons;

namespace SmartLibrary.Utilities.Helpers
{
    public class AutoLogAndToastAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var controller = filterContext.Controller as BaseController;
            if (controller == null) return;

            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            string displayActionName = ActionNameText.GetActionName(actionName);
            string displayControllerName = ControllerNameText.GetControllerName(controllerName);



            if (filterContext.HttpContext.Request.HttpMethod == "POST" && filterContext.Exception == null)
            {
                controller.SetSuccessToast($"{displayActionName} {displayControllerName} thành công!");
                Task.Run(async () =>
                {
                    await controller.LogSuccessAsync(actionName, controllerName, $"Đã thực hiện {actionName} thành công.");
                }).Wait();
            }
            else if (filterContext.Exception != null)
            {
                controller.SetErrorToast($"{displayActionName} {displayControllerName} thất bại!");
                Task.Run(async () =>
                {
                    await controller.LogErrorAsync(actionName, controllerName, filterContext.Exception.Message);
                }).Wait();
            }
        }
    }
}