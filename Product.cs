namespace AlgoliaError;


public record Product(
    string ObjectID,
    Guid Guid,
    string Libelle,
    string ImageBig,
    string ImageSmall,
    string Reference,
    Attribute Marque,
    bool EstDispoWeb,
    bool EstPublie,
    bool EnPromo,
    bool IsEmbargo,
    bool IsPreOrder,
    DateOnly EmbargoDate,
    decimal Prix,
    decimal? PrixOriginal,
    string PourcentagePromo,
    double Note,
    double? AgeEnMoisMin,
    string Link,
    double NotePourFacette,
    decimal NombreVotes,
    IReadOnlyDictionary<string, object> Attributs,
    Statistique Statistiques,
    Tag[] Tags,
    string[]? Keywords,
    string[]? Magasins
);

public readonly record struct Attribute(
    string Code,
    string Name
);

public readonly record struct Tag(
    int Ordre,
    string Html
);

public readonly record struct Statistique(
    int Commandes,
    int Visites,
    int? Position
);