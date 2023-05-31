# ﻿Abbotware.Interop.Iso

Enum classes and metadata for the free ISO Standard Codes:

- [ISO 4217 Currency codes](https://www.iso.org/iso-4217-currency-codes.html) 
- [ISO 3166 Country Codes](https://www.iso.org/iso-3166-country-codes.html)  

The underlying numeric value of the enum matches the numeric code of the ISO standard for the corresponding standard

### Enums

the enums allow for strongly typed code that can use the enum type

```c#
var usd = Currency.USD;
var usa = Country.USA;

switch(curency) 
{
    case Currency.USD : 
        //...

    case Currency.CAD : 
        //...
}
```

### Static Helpers

`IsoHelpers.Country`
`IsoHelpers.Currency`

The static helpers can be used for parsing and metadata lookup

#### Parsing

The TryParse methods can be used to convert a string into the underlying typed enum

```c#
var myRecord = new Data()

//Country 3-Code
if (IsoHelpers.Country.TryParseAlpha3("USA"), out var country)) 
{
	myRecord.Country = country;
}

//Country 2-Code
if (IsoHelpers.Country.TryParseAlpha2("US"), out var country)) 
{
	myRecord.Country = country;
}

// Currency
if (IsoHelpers.Currency.TryParseAlpha3("USD"), out var currency)) 
{
	myRecord.Currency = currency;
}
```

#### Metadata Lookup

included in the lookup and static helpers are metadata classes that provide display name information and other properties for each object type.

```c#
var meta = IsoHelpers.Country[Country.USA];

Console.Write($"Name:{meta.Name} French Name:{meta.NameFrench} Alpha3:{meta.Alpha3} Alpha2:{meta.Alpha2}")
   
```

```c#
var meta = IsoHelpers.Currency[Currency.USD]
    
Console.Write($"Name:{meta.Name} Alpha:{meta.Alpha} MinorUnit:{meta.MinorUnit}")
```

