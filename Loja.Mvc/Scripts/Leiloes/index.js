//Classe estática
const Index = {
    //Consulta ao banco retornando já com o nome, id... etc
    //Aqui a palavra produtos é o suficiente
    viewModel: {
        //Knockaut representado pelo Ko
        //produto é um vetor. Observavel com os binds no html. É tudo dinâmico.
        //Quando ele trocar. O vínculo está no  apply bind
        produtos: ko.observableArray()          //

    },

    inicializar: function () {
        this.conectarLeilaoHub();
        ko.applyBindings(this.viewModel);
    },

    //Criar o método
    conectarLeilaoHub: function () {
        this.conectarLeilaoHub();
    },

    conectarLeilaoHub: function () {
        const self = this;
        const connection = $.hubConnection();
        const hub = connection.createHubProxy("LeilaoHub");

        hub.on("atualizarOfertas", function () { self.obterOfertas(); })

        connection.start();
    },

    obterOfertas: function () {
        this.viewModel.produtos.push({
            //Do mesmo jeito que está lá
            id: 3,
            nome: "Batom",
            preco: 22.03,
            estoque: 3,
            categoriaNome: "Cosméticos"
        });
    }

};

//vetor no csharp é ad. push aqui vetores
//objeto que represena um produto
//Asp.net.api retornam dados
//Asp.net.mvc retornam tela
//O Browser link habilitado faz com que fique sendo atualizado o pool o tempo. Isto é, fica atualizado
//os navegadores existentes no computador para evitar a necessidade do f5 nos trê navegadores se for o caso.
//Vamos transformar em Web.Api.
//Os action não precisam retornar uma tela, pode oferecer o json
//habiltar conceito de api no projeto mvc
//Agora consigo trazer apenas dados

