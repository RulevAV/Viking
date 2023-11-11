import {makeAutoObservable} from "mobx";
import ExerciseModel from "../models/ExerciseModel";
import setService from "../servise/set";
import SetModel from "../models/SetModel";


class Set {
    sets: SetModel[] = [];

    constructor() {
        makeAutoObservable(this);
    }

    async getAllSet(id:string){
        return setService.GetAllSet().then(res=>{
            this.sets = res;
        });
    }

    async createNewSet(idExercise: string) {
        return setService.CreateNewSet(idExercise).then(res => {
            // this.exercise.unshift(res);
            // Workout.workouts = Workout.workouts.map(w => (res.idWorkout !== w.id ? w : {
            //     ...w,
            //     exercises: [...w.exercises, res]
            // }));
        });
    }

    async updateSet(id: string, exerciseName: string) {
        return setService.UpdateSet(id, exerciseName).then(res => {
            // this.exercise = this.exercise.map(t => t.id === res.id ? res : t);
            // Workout.workouts = Workout.workouts.map(w => {
            //     return res.idWorkout !== w.id ? w : {
            //         ...w,
            //         exercises: [...w.exercises.map(e => (e.id === res.id ? res : e))]
            //     }
            // });
        });
    }

    async deleteSet(id: string) {
        return setService.DeleteSet(id).then(res => {
            // this.exercise = this.exercise.filter(t => t.id !== res.id);
            // Workout.workouts = Workout.workouts.map(w=>{
            //     return {
            //         ...w,
            //         exercises: w.exercises.filter(e=> e.id !== res.id)
            //     }
            // })
        });
    }
}

const set = new Set();
export default set;
