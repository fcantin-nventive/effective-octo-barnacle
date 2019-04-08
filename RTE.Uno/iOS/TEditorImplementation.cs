using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TEditor.Abstractions;
using UIKit;

namespace TEditor
{
	public class TEditorImplementation : BaseTEditor
	{
		public override async Task<TEditorResponse> ShowTEditor(string html, ToolbarBuilder toolbarBuilder = null, bool autoFocusInput = false, Dictionary<string, string> macros = null)
		{
			TaskCompletionSource<TEditorResponse> taskRes = new TaskCompletionSource<TEditorResponse>();
			var tvcController = new TEditorViewController();
			ToolbarBuilder builder = toolbarBuilder;
			if (toolbarBuilder == null)
			{
				builder = new ToolbarBuilder().AddAll();
			}

			tvcController.BuildToolbar(builder);
			tvcController.SetHTML(html);
			tvcController.SetAutoFocusInput(autoFocusInput);
			tvcController.Title = CrossTEditor.PageTitle;

			tvcController.SetMacrosDicitionary(macros);

			var tvcWrapper = new UINavigationController(tvcController);
			var nav = (UINavigationController)UIApplication.SharedApplication.KeyWindow.RootViewController;

			tvcController.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(CrossTEditor.CancelText, UIBarButtonItemStyle.Plain, (item, args) =>
			{
				if (nav != null)
				{
					nav.PopViewController(true);
				}

				taskRes.SetResult(new TEditorResponse() { IsSave = false, HTML = string.Empty });
			}), true);

			tvcController.NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(CrossTEditor.SaveText, UIBarButtonItemStyle.Done, async (item, args) =>
			{
				if (nav != null)
				{
					nav.PopViewController(true);
				}

				taskRes.SetResult(new TEditorResponse() { IsSave = true, HTML = await tvcController.GetHTML() });
			}), true);

			tvcWrapper.NavigationBarHidden = false;

			if (nav != null)
			{
				await nav.PresentViewControllerAsync(tvcWrapper, true);
			}

			return await taskRes.Task;
		}
	}
}
