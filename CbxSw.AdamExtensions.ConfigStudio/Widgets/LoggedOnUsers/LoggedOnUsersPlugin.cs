using System;
using System.Collections.Generic;
using Adam.Core.Fields;
using Adam.Core.Search;
using Adam.Core.Server;
using Adam.Web.Extensions.Extensibility;

namespace CbxSw.AdamExtensions.ConfigStudio.Widgets.LoggedOnUsers
{
	/// <summary>
	/// A <see cref="ISearchPluginProvider{T}"/> exposing the active user sessions through 
	/// </summary>
	public class LoggedOnUsersPlugin : SearchPluginProviderBase<SessionInfo>
	{
		/// <summary>
		/// In the constructor, we map the attributes that can be requested and could be returned when calling the SearchPlugin.
		/// </summary>
		public LoggedOnUsersPlugin()
		{
			RegisterPropertyGetter("culture", sessionInfo => sessionInfo.CultureInfo);
			RegisterPropertyGetter("languageId", sessionInfo => sessionInfo.LanguageId);
			RegisterPropertyGetter("languageIdUI", sessionInfo => sessionInfo.LanguageIdForUI);
			RegisterPropertyGetter("languageName", sessionInfo => sessionInfo.LanguageName);
			RegisterPropertyGetter("languageNameUI", sessionInfo => sessionInfo.LanguageNameForUI);
			RegisterPropertyGetter("lastAccessOn", sessionInfo => sessionInfo.LastAccessOn);
			RegisterPropertyGetter("siteId", sessionInfo => sessionInfo.SiteId);
			RegisterPropertyGetter("siteName", sessionInfo => sessionInfo.SiteName);
			RegisterPropertyGetter("id", sessionInfo => sessionInfo.UserId);
			RegisterPropertyGetter("userName", sessionInfo => sessionInfo.UserName);
			// Not exposing sessionInfo.SessionId because that may cause a security risk: the Session ID can be used to logon to ADAM.
			//RegisterPropertyGetter("SessionId", sessionInfo => sessionInfo.SessionId);
		}

		/// <summary>
		/// No fields are available on <see cref="SessionInfo"/>, so returning null.
		/// </summary>
		protected override Func<SessionInfo, IEnumerable<FieldContainer>> GetFieldContainerGetter() => null;

		/// <summary>
		/// Retrieves the data requested by the <paramref name="expression"/> and returns the found results.
		/// </summary>
		protected override IEnumerable<SessionInfo> ExecuteSearch(PluginContext context, SearchExpression expression,
			PagingInfo pagingInfo, IEnumerable<string> properties, string sort, ref bool isLastPage)
		{
			var results = SessionInfo.GetSessions(context.AdamApplication);
			// TODO add paging here
			return results;
		}

		/// <summary>
		/// The default search template to use when the client did not set a search template in the request.
		/// </summary>
		protected override string DefaultSearchTemplate => string.Empty;
	}
}