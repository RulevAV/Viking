import {makeAutoObservable} from "mobx";
import workoutService from "../servise/workout";
import WorkoutModel from "../models/WorkoutModel";


class Workout {
    workouts:WorkoutModel[] = [];
    constructor() {
        makeAutoObservable(this);
    }
    async createNewWorkout(name:string){
        return workoutService.CreateNewWorkout(name).then(res=>{
            this.workouts.unshift(res);
        });
    }
    async getAllWorkout(){
        return workoutService.GetAllWorkout().then(res=>{
            this.workouts = res;
        });
    }
    async updateWorkout(id:string,workoutName:string){
        return workoutService.UpdateWorkout(id,workoutName).then(res=>{
           this.workouts = this.workouts.map(t => t.id === res.id ? res : t);
        });
    }
    async deleteWorkout(id:string){
        return workoutService.DeleteWorkout(id).then(res=>{
           this.workouts = this.workouts.filter(t => t.id !== res.id);
        });
    }
}

const workout = new Workout();
export default workout;
