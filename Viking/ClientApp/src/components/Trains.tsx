import {Component, useEffect} from 'react';
import workout from "../state/workout";
import {observer} from "mobx-react-lite";
import Workout from "./workoutCard/Workout";

const Trains = observer(() => {

  const workouts = workout.workouts

  useEffect(() => {
    workout.getAllWorkout();
  }, []);


  return(<div>
    {workouts?.map(u => {
      const exerciseTabs = u.exercises?.map(t => {
            return {key: t.id, tab:t.exerciseName}
      })
      return <Workout key={u.id} title={u.workoutName} id={u.id} tabs={exerciseTabs}/>
    })}
  </div>)

})

export  default  Trains