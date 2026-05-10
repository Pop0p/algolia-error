// 7.39.0 => OK

// 7.39.1 => KO
// 7.40.0 => KO
// 7.41.0 => KO
// 7.42.0 => KO
// Replace with your own Algolia credentials and index name.
// Adding "hits" to the facets array at row 64 fixes the issue.

using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using AlgoliaError;

var client = new SearchClient("XXX", "XXX");
const string index = "XXX";

string[] allFacetKeys =
[
    "vitrines.name",
    "licence.name",
    "marque.name",
    "prix",
    "notePourFacette",
    "agesEnMois",
    "keywords",
    "categories.lvl0.name",
    "categories.lvl1.name"
];


// Let's say marque.name has been used as an active filter.
var activeFacetKeys = allFacetKeys.Where(x => x == "marque.name").ToArray();
var inactiveFacetKeys = allFacetKeys.Except(activeFacetKeys).ToList();

var searches = new SearchMethodParams
{
    Requests =
    [
        new SearchQuery(new SearchForHits
        {
            IndexName = index,
            Query = "Pokemon",
            Page = 0,
            HitsPerPage = 50,
            Filters = "marque.name: \"Bandai\"",
            AttributesToRetrieve =
            [
                "libelle", "reference", "estDispoWeb", "enPromo", "marque", "estPublie", "vitrines", "magasins",
                "note", "nombreVotes", "prixOriginal", "imageSmall", "link", "pourcentagePromo", "prix", "tags"
            ],
            Facets = [..inactiveFacetKeys],
            AttributesToHighlight = [],
            GetRankingInfo = false,
            ResponseFields = ["page", "nbHits", "nbPages", "hits", "facets", "facets_stats", "renderingContent"]
        }),
        new SearchQuery(new SearchForHits
        {
            IndexName = index,
            Query = "Pokemon",
            HitsPerPage = 0,
            Facets = [..activeFacetKeys],
            AttributesToHighlight = [],
            GetRankingInfo = false,
            ResponseFields = ["facets"], // <= If i add "hits", then it works. 
        })
    ],
};

try
{
    var results = await client.SearchAsync<Product>(searches);
    Console.WriteLine("Success");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}