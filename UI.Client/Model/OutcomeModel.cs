using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace UI.Client.Model
{
    public class OutcomeModel
    {
        private IDictionary<string, double> _results;
        private string _category;
        private TextLoader _loader;

        public OutcomeModel(TextLoader loader, string category, IDictionary<string, double> results)
        {
            if (results == null)
                throw new ArgumentNullException("results");

            if (category == null)
                throw new ArgumentNullException("category");

            if (loader == null)
                throw new ArgumentNullException("loader");

            this._results = results;
            this._category = category;
            this._loader = loader;
        }

        public string Category
        {
            get
            {
                return _category;
            }
        }

        public string Description
        {
            get
            {
                return _loader.TextFor(Category);
            }
        }

        public IDictionary<string, double> SubjectOutcomes
        {
            get
            {
                return _results;
            }
        }
    }
}
