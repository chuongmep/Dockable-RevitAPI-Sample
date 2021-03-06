# DockableRevitAPI
 DockableRevitAPI Sample
Demo : 

![](Image/2020-07-20-13-01-42.gif)

ViewmodeBase : 
```cs
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DockableRevitAPI.Controls
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
```
Command : 

```cs
[Transaction(TransactionMode.Manual)]
	[Regeneration(RegenerationOption.Manual)]
	class Command : IExternalCommand
	{
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;

            if (DockablePane.PaneIsRegistered(App.PaneId))
            {
                DockablePane docpanel = uiapp.GetDockablePane(App.PaneId);

                if (docpanel.IsShown())
                    docpanel.Hide();
                else
                    docpanel.Show();
            }

            else
            {
                return Result.Failed;
			}

			return Result.Succeeded;
		}
	}
```

ViewModel  :
```cs
class DockableViewModel : ViewModelBase
	{
		
		public DockableViewModel(UIControlledApplication a)
		{
            a.ViewActivated += OnViewActivated;
		}

		private void OnViewActivated(object sender, ViewActivatedEventArgs e)
		{
			this.Document = e.Document;
        }

		private Document document;
		public Document Document
		{
			get => document;
            set
			{
				document = value;
				OnPropertyChanged("Document");
			}
		}

	}
```

App : 

```cs
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
```