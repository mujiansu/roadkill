﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Web;

namespace Roadkill.Core.Controllers
{
	/// <summary>
	/// A base controller for all Roadkill controller classes.
	/// </summary>
	public class ControllerBase : Controller
	{
		/// <summary>
		/// Called before the action method is invoked. This overides the default behaviour by 
		/// populating RoadkillContext.Current.CurrentUser with the current logged in user after
		/// each action method.
		/// </summary>
		/// <param name="filterContext">Information about the current request and action.</param>
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			RoadkillContext.Current.CurrentUser = GetCurrentUser();
		}

		/// <summary>
		/// Gets the current logged in user.
		/// </summary>
		/// <returns>The logged in user. Returns an empty string if the user is not logged in</returns>
		protected string GetCurrentUser()
		{
			if (FormsAuthentication.IsEnabled)
			{
				if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
				{
					string cookie = Request.Cookies[FormsAuthentication.FormsCookieName].Value;
					if (!string.IsNullOrEmpty(cookie))
					{
						FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie);
						return ticket.Name;
					}
				}

				return "";
			}
			else
			{
				return Request.LogonUserIdentity.Name;
			}
		}
	}
}
