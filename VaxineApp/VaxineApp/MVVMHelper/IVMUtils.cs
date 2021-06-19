using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.MVVMHelper
{
    public interface IVMUtils
    {
        public void Clear();
        public void CancelSelection();
        public void SaveAsPDF();
        public void Refresh();
        public void GoToPostPage();
        public void GoToPutPage();
        public void GoToDetailsPage();
        public void GoToMapPage();
    }
}
