# Algolia Facet Query Issue

**Problem:** Starting from Algolia client **7.39.1**, response deserialization fails when performing multiple queries search.

**Affected versions:** 7.39.1+

The application performs:

1. One main query to retrieve products with inactive facets (facets not currently used as filters)
2. Additional queries (one per active facet) to retrieve facet values for currently applied filters, allowing users to
   see filtering options even when products share no common facet values

## Workaround

The deserialization fails when performing facet-only searches with ResponseFields containing only
`["facets"]`.

**Solution:** Add `"hits"` to the `ResponseFields` array on active-facet searches, even though hits are not needed.

```csharp
ResponseFields = ["facets", "hits"] // Works
```

Instead of:

```csharp
ResponseFields = ["facets"] // Fails in 7.39.1+
```