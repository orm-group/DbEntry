using System;
using System.Collections.Generic;
using System.Text;
using Leafing.Core.Setting;
using Leafing.Core.Text;
using Leafing.Data.Dialect;
using Leafing.Data.SqlEntry;

namespace Leafing.Data.Builder.Clause {
    [Serializable]
    public class ValuesClause : List<KeyOpValue>, IClause {
        private const string StatementTemplate = "({0}) VALUES ({1})";

        public string ToSqlText(DataParameterCollection dpc, DbDialect dd) {
            var sb1 = new StringBuilder();
            var sb2 = new StringBuilder();
            foreach (var kv in this) {
                string dpStr;
                switch (kv.Operation) {
                    case KvOpertation.None:
                        if (ConfigReader.Config.Database.UseParameter) {
                            dpStr = dd.QuoteParameter(string.Format("{0}_{1}", DataParameter.LegalKey(kv.Key), dpc.Count));
                            var dp = new DataParameter(dpStr, kv.NullableValue, kv.ValueType);
                            dpc.Add(dp);
                        } else {
                            dpStr = DataTypeParser.ParseToString(kv.Value, dd);
                        }
                        break;
                    case KvOpertation.Now:
                        dpStr = dd.DbNowString;
                        break;
                    case KvOpertation.Add:
                    case KvOpertation.Sub:
                        dpStr = "0";
                        break;
                    default:
                        throw new ApplicationException();
                }

                sb1.Append(dd.QuoteForColumnName(kv.Key));
                sb1.Append(",");

                sb2.Append(dpStr);
                sb2.Append(",");
            }
            return string.Format(StatementTemplate, StringHelper.GetStringLeft(sb1.ToString()), StringHelper.GetStringLeft(sb2.ToString()));
        }
    }
}
