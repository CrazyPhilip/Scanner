using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using AForge.Video.DirectShow;

namespace Scanner.ViewModels
{
    class RealTimeCollectingViewModel : NotificationObject
    {
        enum Size
        {
            A4,
            A3
        }

        public DelegateCommand OnCamCommand { get; set; }
        public DelegateCommand ShotCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand ConfirmCommand { get; set; }

        private string batchName;

        public RealTimeCollectingViewModel()
        {
            this.OnCamCommand = new DelegateCommand(new Action(this.OncamCommandExecute));
            this.ShotCommand = new DelegateCommand(new Action(this.ShotCommandExecute));
            this.DeleteCommand = new DelegateCommand(new Action(this.DeleteCommandExecute));
            this.ConfirmCommand = new DelegateCommand(new Action(this.ConfirmCommandExecute));
        }

        private void OncamCommandExecute()
        {

        }

        private void ShotCommandExecute()
        {

        }

        private void DeleteCommandExecute()
        {

        }

        private void ConfirmCommandExecute()
        {

        }

        public string BatchName
        {
            get { return batchName; }
            set
            {
                batchName = value;
                this.RaisePropertyChanged("BatchName");
            }
        }
    }
}
