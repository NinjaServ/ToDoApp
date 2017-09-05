using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.IO;

namespace ToDoApp.Models
{
    class DataTableToCSV
    {
        private void ExportDatatviewToCsv(string iFilename, DataView dv)
        {
            // Open output stream
            StreamWriter swFile = new StreamWriter(iFilename);

            // Header
            string[] colLbls = new string[dv.Table.Columns.Count];
            for (int i = 0; i < dv.Table.Columns.Count; i++)
            {
                colLbls[i] = dv.Table.Columns[i].ColumnName;
                colLbls[i] = GetWriteableValueForCsv(colLbls[i]);
            }

            // Write labels
            swFile.WriteLine(string.Join(",", colLbls));

            // Rows of Data
            foreach (DataRowView rowData in dv)
            {
                string[] colData = new string[dv.Table.Columns.Count];
                for (int i = 0; i < dv.Table.Columns.Count; i++)
                {
                    object obj = rowData[i];
                    colData[i] = GetWriteableValueForCsv(obj);
                }

                // Write data in row
                swFile.WriteLine(string.Join(",", colData));
            }

            // Close output stream
            swFile.Close();
        }

        private void ExportDatatableToCsv(string iFilename, DataTable dt)
        {
            // Open output stream
            StreamWriter swFile = new StreamWriter(iFilename);

            // Header
            string[] colLbls = new string[dt.Columns.Count];

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colLbls[i] = dt.Columns[i].ColumnName;
                colLbls[i] = GetWriteableValueForCsv(colLbls[i]);
            }

            // Write labels
            swFile.WriteLine(string.Join(",", colLbls));

            // Rows of Data
            foreach (DataRow rowData in dt.Rows)
            {
                string[] colData = new string[dt.Columns.Count];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    object obj = rowData[i];
                    colData[i] = GetWriteableValueForCsv(obj);
                }

                // Write data in row
                swFile.WriteLine(string.Join(",", colData));
            }

            // Close output stream
            swFile.Close();
        }

        public static string GetWriteableValueForCsv(object obj)
        {
            // Nullable types to blank
            if (obj == null || obj == Convert.DBNull)
                return "";

            // if string has no ','
            if (obj.ToString().IndexOf(",") == -1)
                return obj.ToString();

            // remove backslahes
            return "\"" + obj.ToString() + "\"";
        }
    }
}

