using System;
using Autodesk.Revit.UI;
using DockableRevitAPI.Controls;

namespace DockableRevitAPI
{
	class App : IExternalApplication
	{
		public static DocPanel DockPanelProvider;
	
        public static DockablePaneId PaneId => new DockablePaneId(new Guid("FAF92697-2CE7-46E0-B7D2-53037BD55507"));

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