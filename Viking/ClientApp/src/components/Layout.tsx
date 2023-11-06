import {FunctionComponent, useEffect} from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';


type Props = {
    title: string,
    paragraph: string,
    children: any
}
const Layout: FunctionComponent<Props> = ({ title, paragraph, children })=> {
    useEffect(()=>{
       // authorize.checkAuthorize();
    },[]);

    return (
      <div>

        <NavMenu />
        <Container tag="main">
          {children}
        </Container>
      </div>
    );
};

export default Layout;
