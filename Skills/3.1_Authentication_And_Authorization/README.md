Use a tool like Postman or a simple client to:

Hit:
Type: GET
Header: Content-Type application/x-www-form-urlencoded
https://localhost:7011/connect/authorize?
client_id=test-client
&response_type=code
&redirect_uri=https://localhost:5001/callback
&scope=openid profile email
&code_challenge=abc
&code_challenge_method=plain

https://localhost:7011/connect/authorize?client_id=test-client&response_type=code&redirect_uri=https://localhost:5001/callback
&scope=openid profile email&code_challenge=abc&code_challenge_method=plain

Log in via Identity UI

Exchange code at:

/connect/token