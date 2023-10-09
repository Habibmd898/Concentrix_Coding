# Concentrix_Coding

- Using the following command to generate a jwt token. go to tools --> Command Prompt and
    dotnet user-jwts create

![image](https://github.com/Habibmd898/Concentrix_Coding/assets/145478624/aaf150e8-5b27-4390-b4fa-117af3d9a6de)


FInally click on Authorize button on swagger and input your token in below format,

Bearer <Token> and click Authorize.

![image](https://github.com/Habibmd898/Concentrix_Coding/assets/145478624/534bb202-066f-46aa-833a-a1fb7f13e14e)


Create a RabbitMQ container by running the following command:

docker run -d --name my_rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
