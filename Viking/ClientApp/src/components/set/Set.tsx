import {observer} from "mobx-react-lite";
import React, {useEffect} from "react";

interface setPropsType {
    id: string,
}

const Set: React.FC<setPropsType>  = observer(({id})=> {
    useEffect(()=>{
        console.log(id)
    },[id])
    return (
        <div >
            test
        </div>
    )
});

export default Set;
