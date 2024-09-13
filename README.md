# CIDA-DotNet8

## Integrantes
- Cauã Alencar Rojas Romero - RM98638
- Jaci Teixeira Santos – RM99627
- Leonardo dos Santos Guerra - RM99738
- Maria Eduarda Ferreira da Mata - RM99004

## Como rodar
- Coloque suas credencias da oracle e azure no arquivo appsettings.json e/ou use as que estão no final do readme
- Execute update database no terminal de gerenciamento de pacotes do NuGet
- Acesse esse link e siga as intruções para rodar a API em python, baixe a branch **(development)**: [API Python](https://github.com/Open-Group-Fiap/CIDA-Python)
- Coloque o arquivo .Env, que foi enviado junto com o projeto, na pasta raiz do projeto em python
- Execute o projeto em python
- Altere a string de conexão da API Python para a URL que a API Python está rodando localmente
- Inicie a aplicação em http


## Escolha da Arquitetura da API
Optamos por utilizar microservices porque a arquitetura já está organizada em duas APIs independentes (C# e Python), com responsabilidades bem definidas. Essa divisão permite escalar cada serviço de forma individual e utilizar a tecnologia mais adequada para cada necessidade. Além disso, a abordagem facilita atualizações e modificações em uma parte do sistema sem impactar o restante da aplicação.

## Benefícios da Arquitetura de Microservices
A escolha por microservices traz diversas vantagens, como a resiliência, onde a falha de um serviço não compromete o funcionamento dos demais. Também ganhamos flexibilidade para adicionar novas funcionalidades ou integrar novos serviços no futuro sem gerar complexidade desnecessária. Por fim, a manutenção se torna mais simples, pois problemas podem ser isolados e corrigidos em módulos específicos, sem afetar todo o sistema.



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
"AzureStorage": "DefaultEndpointsProtocol=https;AccountName=XXXXX;AccountKey=XXXXX;EndpointSuffix=core.windows.net"
}
````