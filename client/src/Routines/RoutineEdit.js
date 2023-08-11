import {useEffect, useState} from "react"
import {useNavigate, useParams} from "react-router-dom"
import {editRoutine, getRoutineById} from "../Managers/RoutineManager"
import {getAllPoses} from "../Managers/PoseManager"

//SEE RoutineFORM FOR SIMILAR CODE AND EXPLANATION
//Notice that useParams is crucial for our edit. This is how we get the data for the specific Routine we are on as well as PUT to the database, overriding the old information with our newly updated inputs
export const RoutineEdit = () => {
    const navigate = useNavigate()
    const localSoulFlyUser = localStorage.getItem("user");
    const soulFlyUserObject = JSON.parse(localSoulFlyUser)
    const { routineId } = useParams();
    const [poses, setPoses] = useState([])
    
    const getPoses = () => {
        getAllPoses().then(allPoses => setPoses(allPoses));
    }
    
    useEffect(() => {
        getPoses()
    }, [])
 
    const [routine, update] = useState({
        intention: "",
        reflection: null,
        cycles: 0,
        userId: soulFlyUserObject.id,
        creationDate: Date.now(),
        poseId: 0
    })

    useEffect(() => {
        getRoutineById(routineId)
        .then((routineArray) => {
            update(routineArray)
        })
    }, [routineId]);

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        const routineToEdit = {
            Id: parseInt(routineId),
            Intention: routine.intention,
            Reflection: routine.reflection,
            Cycles: routine.cycles,
            CreationDate: routine.creationDate,
            PoseId: routine.poseId,
            UserId: routine.userId
        }

        if (routineToEdit.PoseId < 1) {
        return window.alert("There is no routine without a pose!")}
        else{
            return editRoutine(routineToEdit)
            .then(() => {
                navigate(`/routines`)
            })
        } 
    }

    const selectList = (event) => {
        const copy = {
            ...routine
        }
        copy.poseId = event.target.value
        update(copy)
    }

    return (
        <div>
            <form className="routineForm">
                <h2 className="routineForm">New Routine</h2>

                <fieldset>
                    <div className="form-group">
                        <label htmlFor="pose-select">Pose</label>
                        <select id="type"
                            required
                            value={
                                routine.poseId
                            }
                            onChange={
                                event => selectList(event)
                        }>
                            <option value="5">Select your poses</option>
                            {
                            poses.map(pose => {
                                return <option value={pose.id} key={
                                    pose.id
                                }>
                                    {
                                    pose.name
                                }</option>
                        })
                        } </select>  
                        </div>
                </fieldset>
                <fieldset>
                    <div className="form-group">
                        <label htmlFor="intention">Set your Intention</label>
                        <input id="intention" type="text" className="form-control"
                            value={
                                routine.intention
                            }
                            onChange={
                                (event) => {
                                    const copy = {
                                        ...routine
                                    }
                                    copy.intention = event.target.value
                                    update(copy)
                                }
                            }/>
                    </div>
            </fieldset>
            <fieldset>
                <div className="form-group">
                    <label htmlFor="cycle">Cycles</label>
                    <input id="cycle" type="text" className="form-control"
                        value={
                            routine.cycles
                        }
                        onChange={
                            (event) => {
                                const copy = {
                                    ...routine
                                }
                                copy.cycles = event.target.value
                                update(copy)
                            }
                        }/>
                </div>
        </fieldset>


    <button className="btn btn-primary"
        onClick={
            (clickEvent) => handleSaveButtonClick(clickEvent)
    }>
        Save Your Routine</button>
</form></div>
    )


}