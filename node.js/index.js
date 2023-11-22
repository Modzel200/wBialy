let express = require("express");

let app = express();

app.use(express.static("web"));

app.listen(2121,()=>{console.log("Node server is running on http://localhost:2121")})