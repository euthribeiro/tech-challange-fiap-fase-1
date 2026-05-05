# Tech Challange - FASE 01 (DDD)

**Nome do Projeto**: Projeto Chave Inglesa

**Descrição**: Esse projeto aborda a criação de um **Sistema Integrado de Atendimento e Execução de Serviços** aplicando as boas práticas do DDD e desenvolvimento seguro.

## Participantes - Grupo BGT³
| Nome | Matrícula | E-mail | Discord |
|------|-----------|--------|---------|
| Thiago Rodrigues Ribeiro Santana Santos | RM 370291 | thiago_santos14@hotmail.com | thiagoribeiro0611 |
| Bruno da Cruz Barreto | RM 370310 | brunocbarreto2012@gmail.com | bbarreto08 |
| Gabriel Sanchez Fadel Zamai | RM 370308 | gsfzamai@gmail.com | gsfzamai |

## Wrench Auto Repair

<img src="https://github.com/euthribeiro/tech-challange-fiap-fase-1/blob/master/docs/LOGO%20-%20Wrench%20Auto%20Repair.svg" width="350">

## 🚀 Como Executar o Projeto

Os scripts de execução ficam em [`wrench.auto.repair/scripts`](wrench.auto.repair/scripts).

### Subir a aplicação

Ao subir a aplicação, será iniciado um container docker da aplicação e do postgres, estão sendo usada as portas **5432** (database) e **8080** para API, certifique-se se essas duas portas estão disponíveis.

```powershell
docker-compose up
```

### Executar todos os testes (unitários e de integraação)

Serão executado primeiramente todos testes unitários e após serão executados os de integração, ao final da execução de cada projeto de teste é exibida uma estatística de quantidade (testes totais, total de sucesso e total de falhas)

```powershell

docker-compose -f docker-compose.test.yml up --abort-on-container-exit 

```

## A História

### As Origens

Era 2003, em uma cidade do interior paulista, quando Roberto Mendes, então com 28 anos, decidiu transformar sua paixão por motores em negócio. Com as mãos calejadas de anos trabalhando como mecânico em oficinas alheias e algumas economias guardadas a duras penas, ele alugou um galpão pequeno na Rua dos Ipês, comprou um elevador hidráulico usado e pendurou uma placa artesanal na fachada:
> **"Wrench — Consertos com Honestidade"**

O nome veio do inglês mesmo, uma homenagem ao pai, que passava os finais de semana lendo revistas americanas de automobilismo e sempre dizia que uma boa chave de boca — a wrench — era o símbolo do mecânico honesto: simples, confiável e essencial.
Nos primeiros anos, Roberto trabalhava sozinho. Conhecia cada cliente pelo nome, lembrava do histórico de cada carro de cabeça e anotava tudo num caderno azul surrado que ficava sobre o balcão. A qualidade do trabalho correu de boca em boca, e a fila de espera começou a crescer.
A Expansão
Em 2010, Roberto contratou seus primeiros dois mecânicos: Davi, especialista em motores a diesel, e Juliana, a primeira mulher mecânica da cidade, com um talento impressionante para diagnósticos elétricos. A dupla trouxe nova energia à oficina.
O galpão pequeno foi trocado por um espaço maior na Avenida Industrial. A placa ganhou um novo visual, e o nome evoluiu para o que é hoje:

### Wrench Auto Repair

Com isso vieram mais clientes, mais serviços, mais peças em estoque — e também mais desafios. O caderno azul do Roberto não era mais suficiente.
Os Problemas Crescem com o Negócio
Em 2018, a Wrench Auto Repair já contava com 12 funcionários, uma frota de clientes fidelizados e parcerias com seguradoras locais. Mas por dentro, a operação começava a ranger — como um motor sem revisão.
Peças sumiam do estoque sem explicação. Clientes ligavam perguntando o status do carro e ninguém sabia responder com precisão. Orçamentos eram esquecidos em gavetas. Uma vez, um cliente retirou o carro sem que o serviço tivesse sido concluído — simplesmente porque ninguém tinha anotado que ainda faltava a troca do filtro de ar.
Roberto começou a chegar mais cedo e sair mais tarde, apagando incêndios que poderiam ser evitados. Certa noite, sentado na oficina vazia com uma xícara de café frio, ele olhou para o caderno azul — agora o quinto de uma série — e disse em voz alta:
> "Até quando?"

### A Virada

Foi a filha de Roberto, Camila Mendes, recém-formada em Sistemas de Informação, quem trouxe a resposta. Ela convenceu o pai de que a oficina precisava de mais do que planilhas improvisadas — precisava de um sistema integrado, robusto e feito sob medida para a realidade da Wrench.
Com o apoio de uma equipe de desenvolvedores, o projeto foi batizado internamente de "Projeto Chave Inglesa" — uma brincadeira com o nome da oficina — e o desenvolvimento do back-end do sistema começou. A proposta era clara: digitalizar cada etapa do atendimento, desde o momento em que o cliente chega com o carro até a entrega das chaves com o serviço concluído.


## Documentação DDD

No link do Miro abaixo, está toda a documentação relacionada:
- Linguagem Pictográfica
- Jornada AS-IS
- Jornada TO BE (Ordem de Serviço)
- Jornada TO BE (Estoque)
- Contextos Delimitados
- Event Storming

[![Miro Board](https://img.shields.io/badge/Miro-Board-yellow?style=flat&logo=miro)](https://miro.com/app/board/uXjVGuaIszk=/?share_link_id=188169996234)

---

# ADR 001 - Escolha do Banco de Dados

## Status

Aceito

## Contexto

O sistema da Wrench Auto Repair exige persistência consistente e transacional para entidades centrais do domínio, como clientes, veículos, ordens de serviço, peças e usuários.

Além disso, o sistema possui requisitos relevantes:

* Integridade referencial forte (ex: relacionamento entre ordem de serviço e itens)
* Suporte a transações ACID (ex: abertura/fechamento de ordens)
* Evolução controlada de schema (migrações com versionamento)
* Integração com Entity Framework Core e PostgreSQL
* Possível crescimento futuro com necessidade de performance e escalabilidade

Dado esse cenário, a escolha do banco de dados precisa privilegiar consistência, modelagem relacional e previsibilidade operacional.

---

## Decisão

Adotar o PostgreSQL como banco de dados principal do sistema.

---

## Alternativas Consideradas

### MongoDB

* Modelo orientado a documentos
* Flexível para schemas dinâmicos
* Não ideal para forte consistência relacional e transações complexas

### Oracle

* Robusto e altamente performático
* Licenciamento elevado e maior complexidade operacional

### SQL Server

* Excelente integração com stack Microsoft
* Pode gerar custo de licenciamento dependendo do ambiente
* Menor portabilidade em ambientes Linux comparado ao Postgres

### MySQL

* Popular e amplamente utilizado
* Menor aderência a recursos avançados (comparado ao Postgres) em cenários complexos
* Diferenças de comportamento em transações dependendo do engine (InnoDB vs outros)

---

## Consequências

### Positivas

* **Open-source e sem custo de licença**
  Reduz custo operacional e permite uso em qualquer ambiente (dev, staging, produção)

* **Aderência ao modelo relacional**
  Excelente suporte a integridade referencial, constraints e modelagem rica

* **Suporte completo a ACID**
  Essencial para garantir consistência em operações críticas (ex: ordens de serviço)

* **Integração madura com Entity Framework Core**
  Permite uso eficiente de migrations, LINQ e otimizações

* **Recursos avançados**

  * JSONB para dados semi-estruturados
  * Indexação avançada (GIN, GiST)
  * Full-text search
  * Window functions

* **Alta portabilidade e suporte a Linux**
  Ideal para ambientes containerizados (Docker/Kubernetes)

* **Comunidade ativa e documentação extensa**

---

### Negativas

* **Curva de aprendizado para features avançadas**
  Recursos como tuning de queries, indexação e particionamento exigem conhecimento mais profundo

* **Diferenças de comportamento em relação ao SQL Server**
  Pode impactar desenvolvedores acostumados com T-SQL (ex: funções, case sensitivity, sintaxe)

* **Gerenciamento operacional**
  Backup, replicação e tuning exigem configuração explícita (não tão “plug and play” quanto soluções gerenciadas)

* **Ferramentas corporativas mais limitadas**
  Em comparação com Oracle/SQL Server, o ecossistema enterprise pode ser menos integrado

* **Possível necessidade de tuning em escala**
  Para cargas muito altas, será necessário planejar:

  * índices adequados
  * particionamento
  * estratégias de leitura/escrita

---

## Notas Adicionais

* A estratégia inicial será utilizar migrations via Entity Framework Core
* Caso surjam necessidades específicas (ex: busca textual avançada ou cache), outros componentes poderão ser adicionados (ex: Redis, Elastic)
