Supported binding sources:

- Route values
- Query string
- Header
- Body (as JSON)
- Services provided by dependency injection
- Custom

```cs
app.MapGet("/{id}", (int id,
                    int page,
                    [FromHeader(Name = "X-CUSTOM-HEADER")]
                    string customHeader,
                    Service service) => { });
```

| Parameter    | Binding Source                   |
| ------------ | -------------------------------- |
| id           | route value                      |
| page         | query string                     |
| customHeader | header                           |
| service      | Provided by dependency injection |
