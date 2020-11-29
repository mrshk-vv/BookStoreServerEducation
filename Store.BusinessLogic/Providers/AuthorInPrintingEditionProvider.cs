using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Providers
{
    public static class AuthorInPrintingEditionProvider
    {
        public static List<AuthorInPrintingEdition> GetAuthorInPrintingEditionList(List<int> authors, int printingEditionId)
        {
            List<AuthorInPrintingEdition> authorInPrintingEditions = new List<AuthorInPrintingEdition>();
            for (int i = 0; i < authors.Count; i++)
            {
                authorInPrintingEditions.Add(new AuthorInPrintingEdition
                {
                    AuthorId = authors[i],
                    PrintingEditionId = printingEditionId
                });
            }

            return authorInPrintingEditions;
        }

        public static List<AuthorInPrintingEdition> GetAuthorInPrintingEditionList(int authorId , List<int> printingEditions)
        {
            List<AuthorInPrintingEdition> authorInPrintingEditions = new List<AuthorInPrintingEdition>();
            for (int i = 0; i < printingEditions.Count; i++)
            {
                authorInPrintingEditions.Add(new AuthorInPrintingEdition
                {
                    AuthorId = authorId,
                    PrintingEditionId = printingEditions[i]
                });
            }

            return authorInPrintingEditions;
        }

        public static AuthorInPrintingEdition GetAuthorInPrintingEdition(int printingEditionId, int authorId)
        {
            return new AuthorInPrintingEdition
            {
                AuthorId = authorId,
                PrintingEditionId = printingEditionId
            };
        }
    }
}
