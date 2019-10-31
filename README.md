# jaytwo.MiniRouter

What is this for? Well sometimes we want to make middlewares or modules to serve endpoints for things like healthchecks.  

This makes it easier to do a _little bit_ more than serve up content at a single endpoint.  For example, you can serve html 
content for humans at a `/healthcheck` endpoint, and an embeddable badge at `/healthcheck/badge`, a machine-friendly
json output at `/healthcheck/json`.  Or if you're an over-acheiver, you can look at HTTP verbs and requested content type
headers... so you serve html when the requested content type contains `text/html`, and you serve json when it's `application/json`.

TODO: put more stuff here

---

Made with &hearts; by Jake
