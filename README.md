# CIDA-DotNet8

## Integrantes
- Cauã Alencar Rojas Romero - RM98638
- Jaci Teixeira Santos – RM99627
- Leonardo dos Santos Guerra - RM99738
- Maria Eduarda Ferreira da Mata - RM99004

## Como rodar
- Coloque suas credencias da oracle e azure no arquivo appsettings.json e/ou use as que estão no final do readme
- Execute update database no terminal de gerenciamento de pacotes do NuGet
- Inicie a aplicação em http


## Escolha da Arquitetura para o Projeto da API em .NET
No nosso projeto de API em .NET, optamos por utilizar a arquitetura monolítica. Comparada com a arquitetura de microserviços, a arquitetura monolítica se destaca em aspectos como desempenho, implementação e desenvolvimento.

## Desempenho e Implementação
A arquitetura monolítica oferece um desempenho superior devido à ausência de latência entre as camadas da API, uma vez que todas as operações são realizadas em uma única base de código. Além disso, a implementação é facilitada pelo fato de o projeto ser único, simplificando o processo. O desenvolvimento também é menos complexo em uma API monolítica, pois não é necessário criar e gerenciar múltiplas aplicações separadas, como ocorre com a arquitetura de microserviços.


### Diferenças Principais entre Arquitetura Monolítica e Microserviços

1. **Complexidade e Estrutura:**
   - **Monolítica:** Toda a lógica da aplicação está integrada em uma única base de código, o que facilita o desenvolvimento e a gestão inicial.
   - **Microserviços:** A aplicação é dividida em serviços independentes que se comunicam entre si, aumentando a complexidade do sistema.

2. **Escalabilidade:**
   - **Monolítica:** Escala-se a aplicação inteira como um único bloco, o que pode ser ineficiente se apenas uma parte da aplicação estiver sob carga intensa.
   - **Microserviços:** Permite a escalabilidade individual de cada serviço conforme necessário, oferecendo uma escalabilidade mais granular.

3. **Desdobramento e Manutenção:**
   - **Monolítica:** Requer a reimplantação de toda a aplicação para qualquer alteração, o que, embora mais simples, é menos flexível.
   - **Microserviços:** Permite deploys e atualizações independentes de cada serviço, facilitando a manutenção e a inovação sem impactar toda a aplicação.

4. **Comunicação e Desempenho:**
   - **Monolítica:** A comunicação entre componentes é direta e interna, proporcionando melhor desempenho para aplicações menores.
   - **Microserviços:** A comunicação ocorre através de chamadas de rede entre serviços, o que pode introduzir latência e complexidade adicional.

## Design Patterns

1. **Dependency Injection**
2. **Builder Pattern**
3. **Factory Method Pattern**
4. **Adapter Pattern**
5. **Repository Pattern**
6. **Chain of Responsibility Pattern**
7. **Singleton Pattern**

### Credenciais

````json
"ConnectionStrings": {
"FiapOracleConnection": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=XXXXX;Password=XXXXX;",
"AzureStorage": "DefaultEndpointsProtocol=https;AccountName=XXXXX;AccountKey=XXXXX;EndpointSuffix=core.windows.net",
"AzureAIApiKey": "",
"AzureAIEndpoint": "",
"GeminiApiKey": ""
}
````
