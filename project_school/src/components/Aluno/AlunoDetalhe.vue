<template>
    <div v-if="!loading">
        <Titulo :texto="`Aluno: ${aluno.nome}` " :btnVoltar="!visualizando">
            <button v-show="visualizando" class="btn btnEditar" @click="editar()">Editar</button>
        </Titulo>
        

        <table>
            <tbody>
                    <tr>
                        <td class="colPequeno">Matricula:</td>
                        <td>
                            <label>{{aluno.id}}</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="colPequeno">Nome:</td>
                        <td>
                            <label v-if="visualizando">{{aluno.nome}}</label>
                            <input v-else type="text" v-model="aluno.nome" />
                        </td>
                    </tr>
                    <tr>
                        <td class="colPequeno">Sobrenome:</td>
                        <td>
                            <label v-if="visualizando">{{aluno.sobrenome}}</label>
                            <input v-else type="text" v-model="aluno.sobrenome" />
                        </td>
                    </tr>
                    <tr>
                        <td class="colPequeno">Data Nascimento:</td>
                        <td>
                            <label v-if="visualizando">{{aluno.dataNasc}}</label>
                            <input v-else type="text" v-model="aluno.dataNasc" />
                        </td>
                    </tr>
                    <tr>
                        <td class="colPequeno">Professor:</td>
                        <td>
                            <label v-if="visualizando">{{aluno.professor.nome}}</label>
                            <select v-else v-model="aluno.professor">
                                <option v-for="(professor, index) in professores" :key="index" v-bind:value="professor">
                                    {{professor.nome}}
                                </option>    
                            </select>
                        </td>
                    </tr>
            </tbody>
        </table>  

        <div style='margin-top: 10px'>
            <div v-if="!visualizando">
                <button class="btn btnSalvar" @click="salvar(aluno)">Salvar</button>
                <button class="btn btnCancelar" @click="cancelar()">Cancelar</button>
            </div>
        </div> 
             
    </div>
</template>

<script>
    import Titulo from "../_share/Titulo";

    export default {
        components: {
            Titulo
        },

        data() {
            return {
                aluno: {},
                professores: [], 
                idAluno: this.$route.params.id,
                visualizando: true,
                loading: true
            }
        },

        created(){
            this.carregarProfessor();
        },

        methods: {

            carregarProfessor(){
                this.$http
                    .get("http://localhost:5000/api/professor")
                    .then(res => res.json())                          
                    .then(professor => {
                        this.carregarAluno();
                        this.professores = professor
                    });

            },

            carregarAluno(){
                this.$http
                    .get("http://localhost:5000/api/aluno/" + this.idAluno)
                    .then(res => res.json())            
                    .then(aluno => {
                        this.aluno = aluno;
                        this.loading = false;
                    });
            },

            editar(){
                this.visualizando = !this.visualizando;
            },

            salvar(_aluno){
                let _alunoEditar = {
                        id: _aluno.id,
                        nome: _aluno.nome,
                        sobrenome: _aluno.sobrenome,
                        dataNasc: _aluno.dataNasc,
                        professorid: _aluno.professor.id
                }

                this.$http
                    .put(`http://localhost:5000/api/aluno/${_alunoEditar.id}`, _alunoEditar)
                    .then(res => res.json())
                    .then(res => this.aluno = res)
                    .then(() => this.visualizando = true)

                    this.visualizando = !this.visualizando;
            },

            cancelar(){
                this.visualizando = !this.visualizando;
            }
        }
    }
</script>

<style scoped>

    .colPequeno {
        width: 20%;
    }

    input, select {
        margin: 0;
        padding: 5px 10px;
        font-size: 0.9em;
        border-radius: 5px;
        border: 1px solid silver;
        font-family: Montserrat;
        width: 95%;
        background-color: rgb(245, 245, 245);
    }

    select {
        height: 38px;
        width: 100%;
    }

    .btnEditar {
        float: right;
        background-color: rgb(76, 186, 249);
    }

    .btnSalvar {
        float: right;
        background-color: rgb(79, 196, 68);
    }

    .btnCancelar {
        float: left;
        background-color: rgb(249, 186, 92);
    }

</style>