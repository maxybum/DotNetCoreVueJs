<template>
    <v-container fluid>
        <v-slide-y-transition mode="out-in">
            <v-layout column>
                <h1>Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>

                <v-data-table :headers="headers"
                              :items="emp"
                              hide-actions
                              :loading="loading"
                              class="elevation-1">
                    <v-progress-linear v-slot:progress color="blue" indeterminate></v-progress-linear>
                    <template v-slot:items="props">
                        <td>
                            <router-link :to="{ name: 'meeting', params: { id: props.item.id }}"> 
                            {{ props.item.id  }}
                            </router-link>
                        </td>
                        <td>
                            <router-link :to="{ name: 'meeting', params: { id: props.item.id }}">
                                {{ props.item.name }}
                            </router-link>
                        </td>
                    </template>
                </v-data-table>
            </v-layout>
        </v-slide-y-transition>
        <v-alert :value="showError"
                 type="error"
                 v-text="errorMessage">
            This is an error alert.
        </v-alert>
        <v-alert :value="showError"
                 type="warning">
            Are you sure you're using ASP.NET Core endpoint? (default at <a href="http://localhost:5000/fetch-data">http://localhost:5000</a>)<br>
            API call would fail with status code 404 when calling from Vue app (default at <a href="http://localhost:8080/fetch-data">http://localhost:8080</a>) without settings devServer proxy in vue.config.js file.
        </v-alert>
    </v-container>
</template>
<script>

</script>
<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
    import { Employee } from '../models/employee';
    import axios from 'axios';


    @Component({ })

export default class FetchDataView extends Vue {
  private loading: boolean = true;
  private showError: boolean = false;
  private errorMessage: string = 'Error while loading weather forecast.';
  private emp: Employee[] = [];
  private headers = [
    { text: 'Id', value: 'summary' },
    { text: 'Name', value: 'text' },
  ];

  private created() {
      this.fetchEmployees();
      
  }
 
 
  private fetchEmployees() {
      axios
          .get<Employee[]>('api/SampleData/Employees')
          .then((response) => {
              this.emp = response.data;
              console.log(this.emp);
          })
          .catch((e) => {
              this.showError = true;
              this.errorMessage = `Error while loading weather forecast: ${e.message}.`;
          })
          .finally(() => (this.loading = false));

  }
}
</script>
