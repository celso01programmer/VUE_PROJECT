<template>
  <div>
    <Titulo texto="Professores"/>
    
    <table>
      <thead>
          <th>Cód.</th>
          <th>Nome</th>
          <th>Alunos</th>
      </thead> 
      <tbody v-if="Professores.length">
        
        <tr v-for="(professor, index) in Professores" :key="index">
             <td class="colPequeno">{{professor.id}}</td>
            
            <router-link v-bind:to="`/alunos/${professor.id}`" tag="td" style="cursor: pointer">
                {{professor.nome}} {{professor.sobrenome}}
            </router-link>
            
            <td style="text-align: center; width: 15% ">{{professor.qtdAlunos}}</td>
        </tr>

      </tbody> 
      <tfoot v-else>
          Nenhum professor encontrado
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
            Professores: [],
            Alunos: []
        } 
    },
    created(){
      this.$http
        .get("http://localhost:3000/alunos")
        .then(res => res.json())                          
        .then( alunos => {
            this.Alunos = alunos,
            this.carregarProfessores();
          }
      )   

  },
  props: {

  },  
  methods: {
    carregarProfessores(){
      this.$http
        .get("http://localhost:3000/professores")
        .then(res => res.json())                          
        .then(prof => {
            this.Professores = prof
            this.pegarQtdAlunosPorProfessor();
          }
        )   
    },

    pegarQtdAlunosPorProfessor(){
      this.Professores.forEach( (professor, index) => {
        professor = {
          id : professor.id,
          nome : professor.nome,
          qtdAlunos : this.Alunos.filter(aluno => aluno.professor.id == professor.id).length
        }  
        this.Professores[index] = professor;
      });
    }    
  }
}
</script>

<style scoped>

</style>