import Vue from "vue";
import Router from "vue-router";

import Alunos from './components/Aluno/Alunos'
import Professor from './components/Professor/Professor'
import Sobre from "./components/Sobre/Sobre"
import AlunoDetalhe from "./components/Aluno/AlunoDetalhe"

Vue.use(Router);

export default new Router({
    routes: [
        {
            path: "/professores",
            nome: "Professores",
            component: Professor
        },

        {
            path: "/alunos/:prof_id",
            nome: "Alunos",
            component: Alunos
        },

        {
            path: "/alunos",
            nome: "Alunos",
            component: Alunos
        },

        {
            path: "/sobre",
            nome: "Sobre",
            component: Sobre
        },
        {
            path: "/alunoDetalhe/:id",
            nome: "AlunoDetalhe",
            component: AlunoDetalhe

        }

    ]    
});