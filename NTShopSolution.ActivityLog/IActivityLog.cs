using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTShopSolution.ActivityLog
{
    public interface IActivityLog<T>
    {
        void Verbose(string messageTemplate);
        void Verbose(string messageTemplate, params object[] propertyValues);
        void Information(string messageTemplate);
        void Information(string messageTemplate, params object[] propertyValues);
        void Warning(string messageTemplate);
        void Warning(string messageTemplate, params object[] propertyValues);
        void Error(string messageTemplate);
        void Error(string messageTemplate, params object[] propertyValues);
        void Fatal(string messageTemplate);
        void Fatal(string messageTemplate, params object[] propertyValues );
    }
}
