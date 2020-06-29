# jaytwo.MiniRouter

<p align="center">
  <a href="https://jenkins.jaytwo.com/job/jaytwo.MiniRouter/job/master/" alt="Build Status (master)">
    <img src="https://jenkins.jaytwo.com/buildStatus/icon?job=jaytwo.MiniRouter%2Fmaster&subject=build%20(master)" /></a>
  <a href="https://jenkins.jaytwo.com/job/jaytwo.MiniRouter/job/develop/" alt="Build Status (develop)">
    <img src="https://jenkins.jaytwo.com/buildStatus/icon?job=jaytwo.MiniRouter%2Fdevelop&subject=build%20(develop)" /></a>
</p>

<p align="center">
  <a href="https://www.nuget.org/packages/jaytwo.MiniRouter/" alt="NuGet Package jaytwo.MiniRouter">
    <img src="https://img.shields.io/nuget/v/jaytwo.MiniRouter.svg?logo=nuget&label=jaytwo.MiniRouter" /></a>
  <a href="https://www.nuget.org/packages/jaytwo.MiniRouter/" alt="NuGet Package jaytwo.MiniRouter (beta)">
    <img src="https://img.shields.io/nuget/vpre/jaytwo.MiniRouter.svg?logo=nuget&label=jaytwo.MiniRouter" /></a>
</p>

What is this for? Well sometimes we want to make middlewares or modules to serve endpoints for things like healthchecks.  

This makes it easier to do a _little bit_ more than serve up content at a single endpoint.  For example, you can serve html 
content for humans at a `/healthcheck` endpoint, and an embeddable badge at `/healthcheck/badge`, a machine-friendly
json output at `/healthcheck/json`.  Or if you're an over-acheiver, you can look at HTTP verbs and requested content type
headers... so you serve html when the requested content type contains `text/html`, and you serve json when it's `application/json`.

TODO: put more stuff here

---

Made with &hearts; by Jake
