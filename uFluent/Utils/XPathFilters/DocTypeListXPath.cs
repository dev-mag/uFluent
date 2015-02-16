using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TidyNet;

namespace uFluent.Utils.XPathFilters
{
    public class DocTypeListXPath
    {
        private List<string> DocTypes { get; set; } 

        public DocTypeListXPath()
        {
            DocTypes = new List<string>();
        }

        public DocTypeListXPath(string currentXPath)
        {
            DocTypes = currentXPath.Split(new[]{" | "}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public override string ToString()
        {
            return string.Join(" | ", DocTypes);
        }

        public void Add(string docType)
        {
            if (!string.IsNullOrEmpty(docType))
            {
                if(!IsValid(docType))
                {
                    throw new FluentException("Document type alias trying to add is not in a valid format.");
                }

                if(!DocTypes.Contains(docType))
                {
                    DocTypes.Add(docType);
                }
                else
                {
                    throw new FluentException("The document type alias being added already exists.");
                }
            }
            else
            {
                throw new FluentException("Cannot add an empty doc type to XPath Filter list.  The document type alias MUST be specified.");
            }
        }

        public void Remove(string docType)
        {
            if(!string.IsNullOrEmpty(docType))
            {
                if (!IsValid(docType))
                {
                    throw new FluentException("Document type alias trying to remove is not in a valid format.");
                }

                if(DocTypes.Contains(docType))
                {
                    DocTypes.Remove(docType); 
                }
                else
                {
                    throw new FluentException("The document type alias being removed is not listed.");
                }
            }
            else
            {
                throw new FluentException("Cannot remove an empty doc type from XPath Filter list.  The document type alias MUST be specified.");
            }
        }

        public bool IsValid(string value)
        {
            var xPathRegex = new Regex(@"^[a-zA-Z0-9_]+(?:(?:(?:\ \|\ )[a-zA-Z0-9_]+)*)?$");

            return xPathRegex.IsMatch(value);
        }

    }
}