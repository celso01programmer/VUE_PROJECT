<template>
  <div>
    <Titulo :texto="professorid != undefined ?  'Professor: ' + professor.nome : 'Todos os alunos' "/>
    
    <div v-if="professorid != undefined">
      <input type="text" placeholder="Nome do Aluno" v-model="nome" v-on:keyup.enter="addAluno()">
      <button class="btn btnInput" @click="addAluno()">Adicionar</button>
    </div>

    <table>
      <thead>
          <th>Mat.</th>
          <th>Nome</th>
          <th>Opções</th>
      </thead> 
      <tbody v-if="alunos.length">
        
        <tr v-for="(aluno, index) in alunos" :key="index">
           <!-- <td>{{index+1}}</td> -->
            <td class="colPequeno">{{aluno.id}}</td>
            
            <router-link v-bind:to="`/alunoDetalhe/${aluno.id}`" tag="td" style="cursor: pointer">
                {{aluno.nome}} {{aluno.sobrenome}}
            </router-link>


            <td><button class="btn btn_Danger" @click="remover(aluno)">Remover</button></td>
        </tr>

      </tbody> 
      <tfoot v-else>
        <tr>
            <td colspan="3" style="text-align: center"><h5>Nenhum aluno encontrado</h5></td>
        </tr>
    </tfoot>
    </table>
  </div>
</template>

<script>

import Titulo from "../_share/Titulo";

export default {
  
  components: {
    Titulo
  },

  data(){
    return {
      titulo: "Aluno",
      professorid: this.$route.params.prof_id,
      professor: {},
      nome: '',
      alunos: []
          //      {id: 1, nome:"Marcos", sobrenome: "Xavier"},
            //    {id: 2, nome:"Paulo", sobrenome: "Amaral"},
              //  {id: 3, nome:"Lucas", sobrenome: "Henrique"},
    }
  },
  created(){

    if (this.professorid){

      this.carregarProfessor();

      this.$http
        .get(`http://localhost:5000/api/aluno/ByProfessorId/${this.professorid}`)
        .then(res => res.json())          /* converte de text para json */
        .then(aln => this.alunos = aln)   /* o retorno em json é atribuido a this.alunos */
    } else {
      this.$http
        .get("http://localhost:5000/api/aluno")
        .then(res => res.json())          /* converte de text para json */
        .then(aln => this.alunos = aln)   /* o retorno em json é atribuido a this.alunos */
    }
  },
  props: {
  },
  methods: {
    
    addAluno(){

      let _aluno = {
        nome: this.nome,
        sobrenome: "",
        dataNasc: "",
        professorid: this.professor.id
      };

      this.$http
      .post("http://localhost:5000/api/aluno", _aluno)
      .then(res => res.json())              /* retorna em formato texto e convertemos para json */
      .then(alunoRetornado => {             
        this.alunos.push(alunoRetornado);   
        this.nome = '';     
      })
    },

    remover(aluno){

       this.$http
      .delete(`http://localhost:5000/api/aluno/${aluno.id}`)
      .then( () => {
          let indice = this.alunos.indexOf(aluno);
          this.alunos.splice(indice, 1);
      } ) 
 
    },

    carregarProfessor(){
      this.$http
        .get("http://localhost:5000/api/professor/" + this.professorid)
        .then(res => res.json())                          
        .then(prof => {
            this.professor = prof
          }
        )   
    },    
  },
  
}
</script>

<style scoped>

input {
  width: calc(100% - 170px);
  border: 0;
  padding: 10px;
  font-size: 1.3em;
  color: #687f7f;
  display: inline;
}

.btnInput {
  width: 150px;
  border: 0px;
  padding: 10px;
  font-size: 1.3em;
  background-color: rgb(116, 115, 115);
  color: white;
}

.btnInput:hover {
  padding: 10px;
  margin: 0px;
  border: 0px;
}

</style>
