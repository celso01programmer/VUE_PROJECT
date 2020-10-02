<template>
  <div>
    <Titulo texto="Aluno"/>
    
    <div>
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
            <td>{{aluno.id}}</td>
            <td>{{aluno.nome}} {{aluno.sobrenome}}</td>
            <td><button class="btn btn_Danger" @click="remover(aluno)">Remover</button></td>
        </tr>

      </tbody> 
      <tfoot v-else>
          Nenhum aluno encontrado
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
      nome: '',
      alunos: []
          //      {id: 1, nome:"Marcos", sobrenome: "Xavier"},
            //    {id: 2, nome:"Paulo", sobrenome: "Amaral"},
              //  {id: 3, nome:"Lucas", sobrenome: "Henrique"},
    }
  },
  created(){
    this.$http
      .get("http://localhost:3000/alunos")
      .then(res => res.json())          /* converte de text para json */
      .then(aln => this.alunos = aln)   /* o retorno em json é atribuido a this.alunos */
  },
  props: {
  },
  methods: {
    
    addAluno(){

      let _aluno = {
        nome: this.nome,
        sobrenome: ""
      }

      this.$http
      .post("http://localhost:3000/alunos", _aluno)
      .then(res => res.json())              /* retorna em formato texto e convertemos para json */
      .then(alunoRetornado => {             
        this.alunos.push(alunoRetornado);   
        this.nome = '';     
      })
      
             
    },

    remover(aluno){

       this.$http
      .delete(`http://localhost:3000/alunos/${aluno.id}`)
      .then( () => {
          let indice = this.alunos.indexOf(aluno);
          this.alunos.splice(indice, 1);
      } ) 

      
    }
  },
  
}
</script>

<style scoped>

input {
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
