import {NavLink} from 'react-router-dom'
import {Navbar, Nav} from 'react-bootstrap'

export default function Navigation() {
    return (
        <Navbar bg="dark" expand="lg">
            <Navbar.Toggle aria-controls="basic-navbar-nav"/>
            <Navbar.Collapse id="basic-navbar-nav"/>
            <Nav>
                <NavLink className="d-inline p-2 bg-dark text-white" to="/">Home</NavLink>
                <NavLink className="d-inline p-2 bg-dark text-white" to="/users">Users</NavLink>
                <NavLink className="d-inline p-2 bg-dark text-white" to="/albums">Albums</NavLink>
                <NavLink className="d-inline p-2 bg-dark text-white" to="/photos">Photos</NavLink>
            </Nav>
        </Navbar>
    )
}