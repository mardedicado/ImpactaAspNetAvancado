//Kyle Simpson - object literals
var Details = {         //Abriu e fechou chave, trata-se de objeto. 
    leilaoHub: {},      //Essa é uma propriedade
    produtoId: 0,       //o zero para representar que se trata de um número
    connectionId: "",
    nomeParticipante:"",

    //Se o this estiver dentro de outro function o contexto é outro
    //Método abaixo, ou seja, "function". O this se refere ao Details
    inicializar: function (produtoId) {
        this.produtoId = produtoId;
        this.conectarLeilaoHub();
        this.vincularEventos();
    },

    conectarLeilaoHub: function () {

        const self = this
        const connection = $.hubConnection();
        this.leilaoHub = connection.createHubProxy("LeilaoHub");

        //hub.on("atualizarOfertas", function () { document.location.reload(); })
        connection.start().done(function () {
            self.connectionId = connection.id;
        });
    },

    vincularEventos: function () {  //1º é o nome do evento "click", 2º é o que ele faz, neste caso é um método
        const self = this;

        $("#entrarButton").on("click", function () { self.entrarLeilao(); });
        $("#enviarLanceButton").on("click", function () { self.enviarLance(); });

        this.leilaoHub.on("adicionarMensagem", function (nomeParticipante, connectionId, mensagem) {
            self.adicionarMensagem(nomeParticipante, connectionId, mensagem);
        });

        $(document).on("click", "a[data-connection-id]", function () {
            self.enviarLike($(this).data("connection-id"));
        });

        this.leilaoHub.on("receberLike", function (nomeRemetente) { self.receberLike(nomeRemetente);})
    },

    entrarLeilao: function () {
        this.nomeParticipante = $("#nomeParticipante").val();
    
        this.leilaoHub.invoke("Participar", this.nomeParticipante,this.produtoId);

        $("#participanteDiv").hide();
        $("#lanceDiv").show();
        $("#valorLance").focus();
    },

    //Append quer dizer que vai adicionando
    adicionarMensagem: function (nomeParticipante, connectionId, mensagem) {
        $("#lancesRealizadosTable").append(this.montarMensagem(nomeParticipante, connectionId, mensagem));
    },

    montarMensagem: function (nomeRemetente, connectionId, mensagem) {
        var tr = "<tr>";
        tr += "<td>" + nomeRemetente + "</td>";
        tr += "<td>" + mensagem + "</td>";

        var like = "<a data-connection-id='" + connectionId + "' href='#'>" +
                    "<span class='glyphicon glyphicon-thumbs-up' style='font-size:18px'></span></a>";
        var enviadaPorMim = this.connectionId === connectionId;

        tr += "<td>" + (enviadaPorMim ? "" : like) + "</td>";

        tr += "</tr>";

        return tr;
    },

    enviarLance: function () {
        this.leilaoHub.invoke("EnviarLance", this.nomeParticipante, $("#valorLance").val(), this.produtoId);
    },

    enviarLike: function (connectionIdDestinatario) {

        this.leilaoHub.invoke("EnviarLike", this.nomeParticipante,connectionIdDestinatario);        
    },

    receberLike: function (nomeRemetente) {
        $("#sinoNotificacoes")
            .popover("destroy")
            .popover({
                content: "<span class='glyphicon glyphicon-thumbs-up' style='font-size:24px'></span>",
                html: true,
                placement: "left",
                title: nomeRemetente + " diz:"
            })
            .popover("show");
    }    
 };