using System.Text;

namespace SistemaMAV.UI.Web.Helpers {
    public static class References {
        public static string GetReferences_DataTables() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<link rel=\"stylesheet\" href=\"/lib/datatables.net-bs/css/dataTables.bootstrap.min.css\" />");
            sb.AppendLine("<link rel=\"stylesheet\" href=\"/lib/datatables.net-buttons-bs/css/buttons.bootstrap.min.css\" />");
            sb.AppendLine("<link rel=\"stylesheet\" href=\"/lib/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css\" />");
            sb.AppendLine("<link rel=\"stylesheet\" href=\"/lib/datatables.net-responsive-bs/css/responsive.bootstrap.min.css\" />");
            
            sb.AppendLine("<script src=\"/lib/datatables.net/js/jquery.dataTables.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-bs/js/dataTables.bootstrap.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-fixedheader-bs/js/fixedHeader.bootstrap.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-responsive/js/dataTables.responsive.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-responsive-bs/js/responsive.bootstrap.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/jszip/dist/jszip.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-buttons/js/dataTables.buttons.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-buttons-bs/js/buttons.bootstrap.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-buttons/js/buttons.html5.min.js\"></script>");
            sb.AppendLine("<script src=\"/lib/datatables.net-buttons/js/buttons.print.min.js\"></script>");

            return sb.ToString();
        }
    } 
}
