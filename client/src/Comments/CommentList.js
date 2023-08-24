import { useEffect, useState } from "react";
import { getCommentsByRoutineId } from "../Managers/CommentManager.js";
import { getRoutineById } from "../Managers/RoutineManager.js";
import { Button, Card, Col, Container, Row } from "reactstrap";
import { Link, useParams, useNavigate} from "react-router-dom";
import { Comment } from "./Comment.js";
import { deleteComment } from "../Managers/CommentManager.js";

export const CommentList = () => {

    const { id } = useParams();

    const localSoulFlyUser = localStorage.getItem("users");
    const soulFlyUserObject = JSON.parse(localSoulFlyUser);

    const navigate = useNavigate();

    const [comments, setComments] = useState([]);
    const [routine, setRoutine] = useState([]);

    const getComments = (id) => {
        getCommentsByRoutineId(id)
            .then(allComments => setComments(allComments));
    };

    useEffect(() => {
        getComments(id);
    }, [])

    const getRoutine = (id) => {
        getRoutineById(id)
            .then(routine => setRoutine(routine));
    };

    useEffect(() => {
        getRoutine(id);
    }, [])

   

    return (
        <>
            <Container>

                <Link to={`/routines/${id}`}>
                    <strong className="comment-intention">{routine.intention}</strong>
                </Link>
            
                <Row className="comment-row">
                    {comments.map((comment) => (
                        <>
                        <Col md={6} lg={4} key={comment.id}>
                            <Card className="comment-card">
                                <Comment commentProp={comment} getComments={getComments} />
                            </Card>
                        </Col>
                        </>
                    ))}
                </Row>
            </Container>
        </>
    )
}