import {makeAutoObservable} from "mobx";
import ExerciseModel from "../models/ExerciseModel";
import exerciseService from "../servise/exercise";


class Exercise {
    exercise:ExerciseModel[] = [];
    constructor() {
        makeAutoObservable(this);
    }
    async createNewExercise(idWorkout:string,name:string){
        return exerciseService.CreateNewExercise(idWorkout,name);
        //     .then(res=>{
        //     this.exercise.unshift(res);
        // });
    }
    async getAllWorkout(){
        return exerciseService.GetAllWorkout().then(res=>{
            this.exercise = res;
        });
    }
    async updateWorkout(id:string,workoutName:string){
        return exerciseService.UpdateExercise(id,workoutName).then(res=>{
            this.exercise = this.exercise.map(t => t.id === res.id ? res : t);
        });
    }
    async deleteWorkout(id:string){
        return exerciseService.DeleteExercise(id).then(res=>{
            this.exercise = this.exercise.filter(t => t.id !== res.id);
        });
    }
}

const exercise = new Exercise();
export default exercise;
