using System;
using Autodesk.Revit.UI;
using DockableRevitAPI.Controls;

namespace DockableRevitAPI
{
	class App : IExternalApplication
	{
		public static DocPanel DockPanelProvider;
        public static DockablePaneId PaneId => new DockablePaneId(new Guid("D12C5388-69C4-4A27-B440-5AF7AF03D5F1"));

        public static string PaneName => "DocPanel";

		public Result OnStartup(UIControlledApplication app)
		{


            DockPanelProvider = new DocPanel() {DataContext = new DockableViewModel(app)};
            
			if (!DockablePane.PaneIsRegistered(PaneId))
			{
                app.RegisterDockablePane(PaneId,PaneName, DockPanelProvider);
            }

			return Result.Succeeded;
		}

        
		public Result OnShutdown(UIControlledApplication a)
		{
			return Result.Succeeded;
		}
	}
}