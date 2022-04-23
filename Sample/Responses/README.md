Built-in results

| Description                                                                    | Response type                                    | Status Code                                 | API                      |
| ------------------------------------------------------------------------------ | ------------------------------------------------ | ------------------------------------------- | ------------------------ |
| Write a JSON response with advanced options                                    | application/json                                 | 200                                         | Results.Json             |
| Write a JSON response                                                          | application/json                                 | 200                                         | Results.Ok               |
| Write a text response                                                          | text/plain (default), configurable               | 200                                         | Results.Text             |
| Write the response as bytes                                                    | application/octet-stream (default), configurable | 200                                         | Results.Bytes            |
| Write a stream of bytes to the response                                        | application/octet-stream (default), configurable | 200                                         | Results.Stream           |
| Stream a file to the response for download with the content-disposition header | application/octet-stream (default), configurable | 200                                         | Results.File             |
| Set the status code to 404, with an optional JSON response                     | N/A                                              | 404 Results.NotFound                        |
| Set the status code to 204                                                     | N/A                                              | 204 Results.NoContent                       |
| Set the status code to 422, with an optional JSON response                     | N/A                                              | 422 Results.UnprocessableEntity             |
| Set the status code to 400, with an optional JSON response                     | N/A                                              | 400 Results.BadRequest                      |
| Set the status code to 409, with an optional JSON response                     | N/A                                              | 409 Results.Conflict                        |
| Write a problem details JSON object to the response                            | N/A                                              | 500 (default), configurable Results.Problem |
| Write a problem details JSON object to the response with validation errors     | N/A                                              | N/A, configurable                           | Results.ValidationProble |
